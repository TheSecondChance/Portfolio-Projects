<?php
    if (isset($_POST['login-submit']))
    {
        require "dbConnection.php";

        $userID = htmlspecialchars($_POST['userID']);
        $password = htmlspecialchars($_POST['password']);

        if (empty($userID) || empty($password))
        {
            header("Location: ../templates/login.html?error=emptyfields&userID=".$userID);
            exit();
        }
        else
        {
            $statement = $conn->prepare("SELECT * FROM users WHERE username=? OR email=?");
            $statement->bind_param("ss", $userID, $userID);
            $statement->execute();
            $result = $statement->get_result();
            if ($row = $result->fetch_assoc())
            {
                $passwordCheck = password_verify($password, $row['password']);
                if ($passwordCheck)
                {
                    session_start();
                    $_SESSION['userID'] = $row['id'];
                    $_SESSION['userName'] = $row['username'];

                    header("Location: ../templates/page1.php?login=success");
                    exit();
                }
                else
                {
                    header("Location: ../templates/login.html?error=wrongpass&userID=".$userID);
                    exit();
                }
            }
            else
            {
                header("Location: ../templates/login.html?error=nosuchuser&userID=".$userID);
                exit();
            }
        }
    }
    else
    {
        header("Location: ../templates/login.html");
        exit();
    }
?>