<?php
    function open_connection($db = "asu.sport")
    {
        $db_host = "localhost";
        $db_user = "root";
        $db_pass = "";
        // $db = "asu.sport";
        $conn = new mysqli($db_host, $db_user, $db_pass, $db) or die("Connect failed: %s\n". $conn -> error);

        return $conn;
    }

    function close_connection($conn)
    {
        $conn -> close();
    }
?>
