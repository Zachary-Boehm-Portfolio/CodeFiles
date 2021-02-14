<?php
	$movie_name = $_REQUEST["name"];
//Server connection information
$servername = "sql3.freemysqlhosting.net";
$database = "sql3324879";
$username = "sql3324879";
$password = "XTELkYmueW";
//connect to the server
$conn = mysqli_connect($servername , $username , $password, $database);
//check for connection... if failed print why, if success print success
if(!$conn)
{
	die("Connection Failed: " . mysqli_connect_error());
}
//pull in the video link and the image for the name
$sql="SELECT video, name FROM Movies WHERE Movie='$movie_name'";
$result = mysqli_query($conn,$sql);
$row=mysqli_fetch_assoc($result);
$name = $row["name"];
$url = $row["video"];
//pull in the summary, pub, director in
$sql="SELECT Summary, Publication, Director FROM Movies m JOIN Info a ON a.ID = m.ID WHERE Movie='$movie_name'";
$result = mysqli_query($conn,$sql);
$row=mysqli_fetch_assoc($result);
$Summary = $row["Summary"];
$Publication = $row["Publication"];
$Director = $row["Director"];
//close connection
mysqli_close($conn);
?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html>
<head>
<script src="HomePage.js" type="text/javascript"></script>
<link href="MainPage.css" rel="stylesheet" type="text/css" />
<title><?= $movie_name ?></title>
</head>
<!-- Back button for review page -->
<body>
	<button onClick="backButton()" id="BackButton"><img src="BackButton.png" alt="back" id="backButton" /></button>
	<img src="<?= $name ?>" alt="netflix-font" border="0" id="MName">
	<!-- embedded youtube video -->
	<iframe id="video" width="560" height="315" src="<?= $url?>" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
	<!-- table of movie info -->
	<table id="tablesum">
		<tr>
			<td>
				<!-- Display the movie summary-->
				<h3 class="content"><p class="head">Synopsis:</p><?= $Summary ?></h3>
			</td>
		</tr>
		<tr>
			<td>
				<!-- Display The director-->
				<h3 class="content"><p class="head">Director: </p><?= $Director?></h3>
			</td>
		</tr>
		<tr>
			<td>
				<!-- Display the publication (publicator and year) -->
				<h3 class="content"><p class="head">Distripution: </p><?= $Publication?></p>
			</td>
		</tr>
	</table>
	
</body>
</html>