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
    <title>Page #2</title>
</head>

<body>
    <header>
        <form action="../scripts/logout.php" method="post">
            <button type="submit">Выйти</button>
        </form>
    </header>
    <div class="container">
        <h1>Так что же такое <b>Sed bibendum</b>?</h1>
        <h3>Стойте, не выходите! Ведь именно здесь вы узнаете абсолютно всё о <b>Sed bibendum</b>!</h3>
        <p>Учёные Кембриджского университета долго спорили о сущности и предназначении <b>Sed bibendum</b>. В
            результате, они
            решили, что <b>Sed bibendum</b> ut augue id feugiat. Duis lobortis sapien sapien, sollicitudin consectetur
            nunc
            rutrum non. Donec sodales consectetur venenatis. Morbi aliquet nulla nec volutpat sodales. Aenean sodales
            cursus mi, vel dignissim justo volutpat aliquam. Integer eu massa ac sapien dapibus molestie fringilla
            malesuada ex. Suspendisse venenatis, ante ut aliquam maximus, libero ipsum auctor lacus, vitae tincidunt mi
            augue vel sapien. Cras vestibulum molestie quam, sit amet consectetur orci semper eu. Phasellus turpis orci,
            consectetur eget tortor ut, pellentesque ornare risus.</p>
        <p>После проведения невиданного эксперимента под кодовым названием <b>"Ut massa"</b>, британским учёным удалось
            заполучить единственную фотографию <b>Sed bibendum</b>.</p>
        <img src="../img/Sed bibendum.jpg" alt="Sed bibendum" title="Sed bibendum">
        <p>Кроме того, <b>Sed bibendum</b> in accumsan metus nibh, at ullamcorper enim cursus non. Vestibulum ante ipsum
            primis
            in faucibus orci luctus et ultrices posuere cubilia Curae; Nunc gravida nunc nec neque blandit condimentum.
            Aliquam et mi blandit, mattis sem vitae, viverra mi. Praesent venenatis tincidunt vestibulum. Sed facilisis
            sollicitudin erat, eu tempor sapien interdum id. Vivamus finibus nisl eget ligula dapibus dictum. Aenean non
            leo in risus gravida pulvinar non eu velit. Etiam ultricies arcu eu diam iaculis lobortis. Sed eget viverra
            quam.
        </p>
        <p>В результате, учёными были сделаны следующие выводы:</p>
        <ul>
            <li>Aenean eget risus laoreet, ultricies magna tristique, vehicula elit.</li>
            <li>Nullam consectetur dolor in metus ullamcorper, vitae elementum nisi feugiat.</li>
            <li>Nulla nec dui sit amet turpis ultricies vehicula.</li>
            <li>Pellentesque ultricies ante rhoncus, efficitur nulla eu, vestibulum massa.</li>
        </ul>
        <p>Надеемся, вам понравилось читать невероятные рассказы и истории о <b>Sed bibendum</b> и <b>Vestibulum egestas!</b> Наш сайт
            будет развиваться и дополняться всё новыми и новыми статьями. Ещё много тем таких, как: <b>эксперимент "Ut
                massa"</b>, <b>Ut fermentum</b>, <b>Vivamus nunc</b> и <b>Aliquam magna</b> не были раскрыты.
        </p>
        <p>Кстати, вы можете вернуться к прочтению статьи о <b>Vestibulum egestas!</b>, это так невероятно!</p>
        <form action="page1.php" method="post">
            <button type="submit">&#8592; Vestibulum egestas ждёт нас!</button>
        </form>
    </div>
</body>

</html>