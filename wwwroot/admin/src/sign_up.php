<?php
session_start();
require 'connection.php';
$connection = open_connection();

$login = $_POST['login'];
$password = $_POST['password'];
$provider_access_code = $_POST['access_code'];
$self_access_code = md5($login);
$password = password_hash($password, PASSWORD_DEFAULT);

$sql_check_login = "SELECT * FROM users WHERE login = ?";
//$statement_check_login = mysqli_prepare($connection, $sql_check_login);
//mysqli_stmt_bind_param($statement_check_login, 's', $login);
//$query_result = mysqli_stmt_get_result($statement_check_login);
$statement_check_login = $connection->prepare($sql_check_login);
$statement_check_login->bind_param('s', $login);
$statement_check_login->execute();
$query_result = $statement_check_login->get_result();

if ($query_result->num_rows === 0)
{
    $statement_check_login->free_result();

    $sql = "INSERT INTO users (login, type, password, self_access_code, provider_access_code, is_confirmed) 
        VALUES (?, 1, ?, ?, ?, 0)";

    $statement = mysqli_prepare($connection, $sql);
    echo mysqli_error($connection);
    mysqli_stmt_bind_param($statement, 'ssss', $login, $password, $self_access_code, $provider_access_code);

    mysqli_stmt_execute($statement) or die(json_encode([
        "status" => false,
        "type" => "server_error",
        "message" => "Ошибка на стороне сервера",
        "description" => mysqli_error($connection)
    ]));

    $response = [
        "status" => true,
        "type" => "success",
        "message" => "OK"
    ];

    $_SESSION['user'] = [
        "id" => mysqli_insert_id($connection),
        "login" => $login,
        "type" => '1'
    ];
}
else
{
    $response = [
        "status" => false,
        "type" => "username_is_already_taken",
        "message" => "Этот логин занят"
    ];
}
echo json_encode($response);

close_connection($connection);