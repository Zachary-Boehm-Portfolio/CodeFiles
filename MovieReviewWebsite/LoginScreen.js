// JavaScript Document
function createAccount(){
	"use strict";
	// bring in the variables from the account creation form
	var formAccount = document.forms["formAccount"];
	var username = document.getElementById("username1");
	var psw1=document.getElementById("psw1");
	var cpsw=document.getElementById("cpsw");
	var usernames = new Array();
	//check to see if the password and confirm passwords are the same along with all boxes are filled
	if(psw1.value === ''){
	   psw1.style.color = "red";
	}else{
	   psw1.style.color = "black";
	}
	if(cpsw.value === ''){
	   cpsw.style.color = "red";
	}else{
	   cpsw.style.color = "black";
	}
	if(username.value === ''){
	   username.style.color = "red";
	}else{
		username.style.color = "black";
		//only check if the passwords are valid after its confirmed there is a username
		if(psw1.value === cpsw.value){
		//send to create account php and check if the username is taken. if not add to file then send to login
		formAccount.submit();
		}else{
		cpsw.style.color = "red";
		}
	}
	
}
function login(){
	"use strict";
	//send the login information
	// first pull in the username and password
	var username = document.getElementById("username2");
	var password = document.getElementById("psw2");
	// check if they are empty
	if(username.value === ''){
		username.style.color = "red";
	}else if (password.value === ''){
		username.style.color = "#221f1f";
		password.style.color = "red";
	}else{
		//else both inputs are filled then reset text color and submit form
		username.style.color = "#221f1f";
		password.style.color = "#221f1f";
		var loginForm = document.forms["formLogin"];
		loginForm.submit();
	}
	
	
}
function loginPage(){
	"use strict";
	// Bring up the login block form and make the account creation block form disapear
	document.getElementById("form-account").style.display = "none";
	document.getElementById("form-login").style.display = "block";
}
function back(){
	"use strict";
	// bring up the account creation block form and make the login form block disapear
	document.getElementById("form-account").style.display = "block";
	document.getElementById("form-login").style.display = "none";
	
}
function created(){
	"use strict";
	//Account created... redirect user to login screen
	window.location = "Login Screen.html";
}