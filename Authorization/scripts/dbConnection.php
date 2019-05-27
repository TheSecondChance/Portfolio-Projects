<?php
$ServerName = "localhost";
$Username = "TheSecondChance";
$Password = "r1qz4IWrPir7WXIx";
$Database = "login_system";

$conn = new mysqli($ServerName, $Username, $Password, $Database);

if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
} 
?>