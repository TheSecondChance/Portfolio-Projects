<?php
    session_start();
    if (!isset($_SESSION['userID']))
    {
        header("Location: ../templates/login.html?error=loginfailed");
        exit();
    }
?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <link href="https://fonts.googleapis.com/css?family=Montserrat" rel="stylesheet">
    <link rel="stylesheet" href="../css/page.css">
    <title>Page #1</title>
</head>

<body>
    <header>
        <form action="../scripts/logout.php" method="post">
            <button type="submit">Выйти</button>
        </form>
    </header>
    <div class="container">
        <?php
            $user = $_SESSION['userName'];
            printf("<h1>Добро пожаловать на наш сайт, %s!</h1>", $user);
        ?>
        <h3>Здесь вы найдёте огромное количество случайно сгенерированного текста, наслаждайтесь!!!</h3>
        <p>Давайте поговорим о <b>Vestibulum egestas</b>.</p>
        <p>В впервую очередь стоит отметить, что <b>Vestibulum egestas</b>, lorem a aliquet facilisis, arcu nibh tempor
            lorem, sed
            ultrices purus nisi in diam. Maecenas vel ultrices est. Etiam neque risus, placerat vel nulla ac,
            sollicitudin
            elementum urna. Sed mauris est, aliquet ut lacus nec, consectetur condimentum urna. Etiam ornare faucibus
            auctor. Fusce rhoncus dolor pulvinar risus pellentesque ullamcorper. Integer at est ac quam eleifend
            pretium.
            Pellentesque molestie, dolor nec pretium tempus, quam mi aliquet est, eget finibus lacus nisl sed ex.
            Maecenas
            tincidunt et turpis et aliquet.
        </p>
        <p><b>Vestibulum egestas</b> харакреризуется по следующим пунктам:</p>
        <ul>
            <li>Phasellus vitae arcu eget leo mattis aliquet sed ac lacus.</li>
            <li>Mauris vel neque id dolor ultrices laoreet.</li>
            <li>Nam quis turpis fermentum, consequat nulla eu, rhoncus sapien.</li>
            <li>Aenean ac sem sed sem sagittis posuere quis ornare ante.</li>
            <li>Nunc quis ante eget ante aliquam auctor vel dapibus libero.</li>
        </ul>
        <p>Чтобы разобраться, что же всё-таки такое <b>Vestibulum egestas</b>, нужно руководствоваться следующего плана:</p>
        <ol>
            <li>Sed blandit augue non sodales cursus.</li>
            <li>Quisque id magna ultrices, sodales magna nec, feugiat nibh.</li>
            <li>Curabitur pretium mauris et vestibulum suscipit.</li>
            <li>Vestibulum quis massa at turpis egestas ornare.</li>
            <li>Donec egestas mi vitae augue dictum, porttitor tincidunt nulla pulvinar.</li>
            <li>Donec dignissim augue sit amet massa consectetur, in efficitur felis imperdiet.</li>
            <li>Etiam ullamcorper sapien ac tempor porta.</li>
        </ol>
        <p>Вы, наверное, могли заметить, что при рассказе о <b>Vestibulum egestas</b> мы не раз упомянули <b>Sed
                bibendum</b>.
        </p>
        <p>
        <a href="page2.php">Здесь</a> вы можете узнать об этой замечательной вещи.
        </p>
    </div>
</body>

</html>