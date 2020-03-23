<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>Register</title>
    <link rel="stylesheet" href="css/loginform.css">
</head>
<body onload="document.getElementById('id01').style.display='block'">


     <div id="id01" class="modal">
         
        <form class="modal-content animate" >
            <div class="imgcontainer">
                <span onclick="location.href = 'Index.html'" class="close" title="Return">&times;</span>
                <h1> Register </h1>
                <div>
                     
                </div>
               
                <label><b>Upload Image</b></label>
                <i class="fa fa-camera upload-button"></i>
                    <input class="file-upload" type="file" accept="image/*"/>
            </div>
            <label class="login">Course: </label>
            <div class="container">
                <select class="login" name="course">
                    <option value="male">Computer Science</option>
                    <option value="female">Maths</option>
                    <option value="other">Electronic Engineering</option>
                </select>

                 <label class="login"> Would you like to register as a driver?: </label>
                <input id="box" type="checkbox" checked="checked" name="driver" onclick="driver()"/>
               
                
                <button type="submit">Login</button>
                
            </div>

            
        </form>
         
    </div>
   
    
     <script>

        window.onbeforeunload = function (e) {
          return 'Are you sure?';
         };

         $(document).ready(function() {

    
        var readURL = function(input) {
                if (input.files && input.files[0]) {
                    var reader = new FileReader();

                    reader.onload = function (e) {
                        $('.profile-pic').attr('src', e.target.result);
                    }
    
                    reader.readAsDataURL(input.files[0]);
                }
            }
    

            $(".file-upload").on('change', function(){
                readURL(this);
            });
    
            $(".upload-button").on('click', function() {
               $(".file-upload").click();
            });
        });

    </script>

</body>
</html>