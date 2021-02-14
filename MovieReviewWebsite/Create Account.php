<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html>
<head>
<link href="Login Screen.css" rel="stylesheet" type="text/css" />
<script src="LoginScreen.js" type="text/javascript"></script>
<title>Creating Account</title>
</head>

<body>
	<?php
		//bring in the variables from the form submittion
		$username=$_POST["username"];
		$password=$_POST["psw"];
		$confirmpsw=$_POST["cpsw"];
		$usernames = array();
		$passwords = array();
		$infoFound = false;
	?>
	<!-- Displaying the information back to user -->
	<?php
	//pull in all the usernames and passwords from text file
	$userFile = fopen("users.txt", "r") or die("Unable to open file!");
	//while there are lines to read
	while(!feof($userFile)){
		// grab the line
		$tempinfo = fgets($userFile);
		// split the line into username and password
		$info = explode(" ", $tempinfo);
		$usernames[] = $info[0];
		$passwords[] = $info[1];
	}
	//close file
	fclose($userFile);
	//check for a username match
	for($i = 0;$i < count($usernames); $i++){
		// compare usernames array with the username given
		if($usernames[$i] == $username){
			// if the username is found then it already exists break when found
			$infoFound = true;
			break;
		}
	}
	// if username match then not a valid username... try again
	if($infoFound){
		// if username is already taken then redirect back to login else add user to file
		header("Location: Login Screen.html");
	}else{
		// open users file
		$userFile = fopen("users.txt", "a") or die("Unable to open file!");
		// create the username and password string to write to users file
		$newUser = ("\r\n" . $username . " " . $password);
		// write the new user information to users file
		fwrite($userFile, $newUser);
		// close users file
		fclose($userFile);
		?>
	<br>
	<?php
		}
	//Display that account was created successfully and then prompt user to go login
	?>
	<div id="Success" class="Success">
		<h2 id="h21">Account Created Successfully!</h2>
		<h2 id="h22">To Access Website Please Login</h2>
		<button id="logging" onClick="created()">Logins</button>
	</div>
</body>
</html>