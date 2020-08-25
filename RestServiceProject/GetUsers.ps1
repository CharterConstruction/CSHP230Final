$baseUri = "http://localhost:1416/api/"
$userUri = $baseUri + "Users"


$newUser = 
'{
"UserEmail"    : "test@api.com",
"Password" : "password123",
"FirstName"    : "API",
"LastName"     : "POST"
}'

#$JSON = ConvertTo-JSON($newUser)


Invoke-RestMethod -Method Post -Uri $userUri -body $newUser

	
#GET to get all users
    #Invoke-RestMethod -Method Get -Uri $userUri

#GET {guid} to get a single user
#DELETE {guid} to delete a single user
#Create Postman requests for each action (export your requests and save it to the repo)



