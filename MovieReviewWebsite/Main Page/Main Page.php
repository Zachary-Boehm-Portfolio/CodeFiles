
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html>
<head>
<link href="MainPage.css" rel="stylesheet" type="text/css" />
<script src="HomePage.js" type="text/javascript"></script>
<meta name="viewport" content="width=device-width,user-scalable=0">
<title>Main Page</title>
</head>
<body>
	<table cellspacing="8">
	<tr>
		<td>
			<!-- Comedy Category Image -->
			<img src="https://fontmeme.com/permalink/200302/e442592b28924083b4a328d09e3e5ff6.png" alt="netflix-font" border="0" class="category">
		</td>
		<td/><td/><td/></td><td><button id="loginbutton" onClick="logins()">Logins</button></td>
	</tr>
	<tr>
		<?php

			//Server connection information
			$servername = "sql3.freemysqlhosting.net";
			$database = "sql3324879";
			$username = "sql3324879";
			$password = "XTELkYmueW";
			//connect to the server
			$connect = mysqli_connect($servername , $username , $password, $database);
			//check for connection... if failed print why, if success print success
			if(!$connect)
				{
					die("Connection Failed: " . mysqli_connect_error());
				}
			// request for cover image/poster link, link to the php page when clicked, and the alt fo the image
			$sqli="SELECT Poster, link, alt FROM PosterView WHERE category='Comedy'";
			$results = mysqli_query($connect,$sqli);
			//display the images and there info into a hyperlink in this one table row for all comedy movies
			while($row = mysqli_fetch_assoc($results)){
				?>
				<td>
					<a href="<?= $row['link']?>" class="poster"><img src="<?= $row['Poster'] ?>" alt="<?= $row['alt']?>" class="cover"/></a>
				</td>
		<?php
			}
		?>
		</tr>
		<tr>
		<td>
			<!-- Action Category Image -->
			<img src="https://fontmeme.com/permalink/200303/a45b4ef7ef346e1ae710e8b65723d843.png" alt="netflix-font" border="0" class="category">
		</td>
	</tr>
		<tr>
			<?php
			
			// pull all information to display the action movies hyperlinks
			$sqli="SELECT Poster, link, alt FROM PosterView WHERE category='Action'";
			$results = mysqli_query($connect,$sqli);
			
			while($row = mysqli_fetch_assoc($results)){
				?>
				<td>
					<a href="<?= $row['link']?>" class="poster"><img src="<?= $row['Poster'] ?>" alt="<?= $row['alt']?>" class="cover"/></a>
				</td>
			<?php
			}
		?>
		</tr>
		<tr>
		<td>
			<!-- Thriller Category Image -->
			<img src="https://fontmeme.com/permalink/200303/3459909e622d84622f0888149b8b780a.png" alt="netflix-font" border="0" class="category">
		</td>
		</tr>
		<tr>
			<?php
			
			//pull in the information to display the thriller movies hyperlinks
			$sqli="SELECT Poster, link, alt FROM PosterView WHERE category='Thriller'";
			$results = mysqli_query($connect,$sqli);
			
			while($row = mysqli_fetch_assoc($results)){
				?>
				<td>
					<a href="<?= $row['link']?>" class="poster"><img src="<?= $row['Poster'] ?>" alt="<?= $row['alt']?>" class="cover"/></a>
				</td>
			<?php
			}
			?>
		</tr>
		<tr>
		<td>
			<!-- Murder Mystery Category Image -->
			<img src="https://fontmeme.com/permalink/200303/6c8d18699651d77ad976447b057a0e9e.png" alt="netflix-font" border="0">
		</td>
		</tr>
		<tr>
			<?php
			
			// pull in the information to display the murder mystery movies hyperlinks
			$sqli="SELECT Poster, link, alt FROM PosterView WHERE category='Murder Mystery'";
			$results = mysqli_query($connect,$sqli);
			
			while($row = mysqli_fetch_assoc($results)){
				?>
				<td>
					<a href="<?= $row['link']?>" class="poster"><img src="<?= $row['Poster'] ?>" alt="<?= $row['alt']?>" class="cover"/></a>
				</td>
			<?php
			}
			?>
		</tr>
		<tr>
		<td>
			<!-- Horror Category Image -->
			<img src="https://fontmeme.com/permalink/200303/a2e11a865c1aab3568605878afb15406.png" alt="netflix-font" border="0" class="category">
		</td>
		</tr>
		<tr>
			<?php
			
			//pull in the information to display the horror movies hyperlinks
			$sqli="SELECT Poster, link, alt FROM PosterView WHERE category='Horror'";
			$results = mysqli_query($connect,$sqli);
			
			while($row = mysqli_fetch_assoc($results)){
				?>
				<td>
					<a href="<?= $row['link']?>" class="poster"><img src="<?= $row['Poster'] ?>" alt="<?= $row['alt']?>" class="cover"/></a>
				</td>
			<?php
			}
			mysqli_close($connect);
			?>
		</tr>
	</table>
		
</body>
</html>
