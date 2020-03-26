<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>Add Car</title>
    <link rel="stylesheet" href="css/loginform.css">
</head>

<body onload="document.getElementById('id01').style.display='block'">


     <div id="id01" class="modal">
         
        <form class="modal-content animate" action="">
            <div class="imgcontainer">
                <span onclick="location.href = 'Index.html'" class="close" title="Return">&times;</span>
                <h1> Add Vehicle </h1>
                
            </div>

            <div class="container">
                

                <label class="login"> Registration Number: </label>
                <input class="login" type="text" name="forename" placeholder="Enter Reg Plate Number" required/>
                <label class="login"> Make: </label>
                <input class="login" type="text" name="surname" placeholder="Enter Car Make" required/>
                <label class="login"> Model: </label>
                <input class="login" type="text" name="surname" placeholder="Enter Car Model" required/>
               
                <label class="login"> Number of Seats: </label>
                <select class="login" name="numOfSeats">
                    <option value="2">2</option>
                    <option value="3">3</option>
                    <option value="4">4</option>
                    <option value="5">5</option>
                </select>

                <input type="submit" id="submitBtn" value="Next" />
                
            </div>

            
        </form>
         
    </div>
</body>
</html>