<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html>
<head>
<title>Home Page</title>
<script src="LoginScreen.js" type="text/javascript"></script>
</head>

<body style= "background-color: #221f1f">
	<?php
		$username=$_POST["username2"];
		$password=$_POST["psw2"];
	?>
	<!-- Display the information back to the user -->
	<?php
	//pull in all the usernames and passwords from text file
	$userFile = fopen("users.txt", "r") or die("Unable to open file!");
	while(!feof($userFile)){
		$tempinfo = fgets($userFile);
		$info = explode(" ", $tempinfo);
		$usernames[] = $info[0];
		$passwords[] = $info[1];
	}
	fclose($userFile);
	//check for a username match
	$p;
	$infoFound = false;
	for($i = 0;$i < count($usernames); $i++){
		if($usernames[$i] == $username){
			$p = $i;
			$infoFound = true;
			break;
		}
	}
		//if username is found check for the correct password and if it is bad then try again... or log user in
	if($infoFound){
		if($password == intval($passwords[$p])){
			header("Location: http://mscs-php.uwstout.edu/2020SP/cs/248/001/boehmz8613/Assignment%209/Main%20Page/Main%20Page.php");
		}
	}else{
			header("Location: Login Screen.html");
	}
	?>
</body>
</html>