<?php
session_start();
require 'connection.php';
$connection = open_connection();

$login = $_POST['login'];
$password = $_POST['password'];

$sql = "SELECT * FROM users WHERE login = ?";
//$statement = mysqli_prepare($connection, $sql);
//mysqli_stmt_bind_param($statement, 'ss', $login, $password);
//mysqli_stmt_execute($statement);
//$user_query_result = mysqli_stmt_get_result($statement);
$statement = $connection->prepare($sql);
$statement->bind_param('s', $login);
$statement->execute();
$user_query_result = $statement->get_result();
//echo $user_query_result->num_rows.$password;
if ($user_query_result->num_rows > 0)
{
    $user = $user_query_result->fetch_assoc();
    if (password_verify($password, $user['password']))
    {
        $_SESSION['user'] = [
            "id" => $user['id'],
            "login" => $user['login'],
            "type" => $user['type']
        ];

        $response = [
            "status" => true
        ];
    }
    else {
        $response = [
            "status" => true,
            "type" => "wrong_password",
            "message" => "Неверный пароль"
        ];
    }
}
else
{
    $response = [
        "status" => false,
        "type" => "no_user"
    ];
}
echo json_encode($response);

close_connection($connection);