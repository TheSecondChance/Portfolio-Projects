<?php
    if (isset($_POST['submit']))
    {
        require "dbConnection.php";

        $username = htmlspecialchars($_POST['username']);
        $email = htmlspecialchars($_POST['email']);
        $password = htmlspecialchars($_POST['password']);
        $passwordRetry = htmlspecialchars($_POST['passwordRetry']);

        if (empty($username) || empty($email) || empty($password) || empty($passwordRetry))
        {
            header("Location: ../index.html?error=emptyfields");
            exit();
        }
        else if ($password != $passwordRetry)
        {
            header("Location: ../index.html?error=passwordsunequal");
            exit();
        }
        else if (!preg_match("/^\w+$/", $username))
        {
            header("Location: ../index.html?error=invaliduname");
            exit();
        }
        else if (!filter_var($email, FILTER_VALIDATE_EMAIL))
        {
            header("Location: ../index.html?error=invalidemail");
            exit();
        }
        else if (preg_match("/^[A-Za-z0-9!#$%&'*+\-\/=?^_`{|}~.]+$/", $password))
        {
            $requiredLength = 5;
            if (strlen($password) < $requiredLength)
            {
                header("Location: ../index.html?error=smallpass");
                exit();
            }
            else if (!preg_match("/\d/", $password))
            {
                header("Location: ../index.html?error=nodigits");
                exit();
            }
            else if (!preg_match("/[A-Za-z]/", $password))
            {
                header("Location: ../index.html?error=noletters");
                exit();
            }
            else
            {

                if ($statement = $conn->prepare("SELECT * FROM users WHERE username=?"))
                {
                    $statement->bind_param("s", $username);
                    $statement->execute();
                    $statement->store_result();

                    if ($statement->num_rows > 0)
                    {
                        header("Location: ../index.html?error=userexists");
                        exit();
                    }
                    else
                    {
                        // Создаём нового пользователя
                        $statement = $conn->prepare("INSERT INTO users (username, email, password) VALUES (?,?,?)");
                        $hash = password_hash($password, PASSWORD_DEFAULT);
                        $statement->bind_param("sss", $username, $email, $hash);
                        $statement->execute();

                        header("Location: ../templates/login.html?signup=success&userID=".$username);
                        exit();
                    }
                }
            }
        }
        else
        {
            header("Location: ../index.html?error=invalidpass");
            exit();
        }
    }
    else
    {
        header("Location: ../index.html");
        exit();
    }
?>
