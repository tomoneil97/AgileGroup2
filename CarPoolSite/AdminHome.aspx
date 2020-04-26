<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminHome.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>
<html>
<head>
     <link rel="stylesheet" href="css/site.css">
    <style>
        /* Set the size of the div element that contains the map */
        #map {
            
            height: 2000px; /* The height is 400 pixels */
            max-height: 75%;
            width: 100%; /* The width is the width of the web page */
            position:absolute;
            top: 67px;
            left: 8px;
        }
    </style>
    
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js">
        </script>
</head>

<body onload="document.getElementById('destinationModal').style.display='block'">
    <div>
        <nav class="topbar">
            
            <input type="image" src="<%=img %>" alt="icon" class="userIcon" onclick="openNav()"><a href="#" class="notification"><span>Inbox</span>
                 <div id="notifDiv" runat="server">
                     <span class="badge"><% =notifNum %></span>
                 </div>

            </a> 
        </nav>
    </div>
    

    <div id="mySidenav" class="sidenav">
        <br />
        <a href="Home.aspx">Home</a>
        <a href="ManageUsers.aspx">Manage Users</a>
        <a href="ManageVehicles.aspx">Manage Vehicles</a>
        <a href="UsageStats.aspx">Usage Statistics</a>
        <br /><br /><br />
        <a asp-page="./A_Settings">Admin Settings</a>
        <a asp-page="./SignOut">Sign Out</a>
    </div>

    <div id="adminScreen" class="adminBox">

    </div>

    <script>
        function openNav() {
            if (document.getElementById("mySidenav").style.width == "250px") {
                closeNav();
            }
            else {
                document.getElementById("mySidenav").style.width = "250px";
            }


        }

        function closeNav() {
            document.getElementById("mySidenav").style.width = "0";
        }
    </script>

    <script>
        targetElement.ontouchend = (e) => {
            e.preventDefault();
        };
    </script>



    <!--The div element for the map -->
    <script>
        // Initialize and add the map
        function initMap() {
            var directionsRenderer = new google.maps.DirectionsRenderer;
            var directionsService = new google.maps.DirectionsService;
            var matrixService = new google.maps.DistanceMatrixService();



            // The location of Hull Uni
            var campus = { lat: 53.7730979, lng: -0.3669859 };

            // The map, centered at Uni
            var map = new google.maps.Map(
                document.getElementById('map'), { zoom: 14, center: campus });
            directionsRenderer.setMap(map);
            infoWindow = new google.maps.InfoWindow;
            // The marker, positioned at Uluru
            var marker = new google.maps.Marker({ position: campus, map: map });
            <% =markersString%>
            // Try HTML5 geolocation.
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(function (position) {
                    pos = {
                        lat: position.coords.latitude,
                        lng: position.coords.longitude
                    };
                    stringpos = position.coords.latitude + ", " + position.coords.longitude
                    infoWindow.setPosition(pos);
                    infoWindow.setContent('Current Location');
                    infoWindow.open(map);
                    //calculateAndDisplayRoute(directionsService, directionsRenderer, matrixService);
                    //map.setCenter(pos);
                }, function () {
                    handleLocationError(true, infoWindow, map.getCenter());
                });
            } else {
                // Browser doesn't support Geolocation
                handleLocationError(false, infoWindow, map.getCenter());
            }

            
        var closeBtn = document.getElementById("closeBtn");

        closeBtn.onclick = function () {
            
            calculateAndDisplayRoute(directionsService, directionsRenderer, matrixService);
            document.getElementById('destinationModal').style.display='none';
        }
        }

        function handleLocationError(browserHasGeolocation, infoWindow, pos) {
            var modal = document.getElementById("myModal");
            modal.style.display = "none";
            var noGPS = document.getElementById("noGPS");
            noGPS.style.display = "block";
            infoWindow.setPosition(pos);
            infoWindow.setContent(browserHasGeolocation ?
                'Error: The Geolocation service failed.' :
                'Error: Your browser doesn\'t support geolocation.');
            infoWindow.open(map);


        };

        function calculateAndDisplayRoute(directionsService, directionsRenderer, matrixService) {
             
            var dest = document.getElementById("destination").value;
            var sel = document.getElementById("destination");
             place = sel.options[sel.selectedIndex].text;
            directionsService.route(
                {
                    origin: pos,
                    destination: dest,
                    travelMode: 'DRIVING'
                },
                function (response, status) {
                    if (status === 'OK') {
                        directionsRenderer.setDirections(response);
                    } else {
                        window.alert('Directions request failed due to ' + status);
                    }
                });

            matrixService.getDistanceMatrix(
                {
                    origins: [pos],
                    destinations: [dest],
                    travelMode: 'DRIVING'
                }, callback);




        }

        function callback(response, status) {
            if (status == 'OK') {
                var origins = response.originAddresses;
                var destinations = response.destinationAddresses;

                for (var i = 0; i < origins.length; i++) {
                    var results = response.rows[i].elements;
                    for (var j = 0; j < results.length; j++) {
                        var element = results[j];
                        var distance = element.distance.text;
                        var duration = element.duration.text;
                        durationValue = element.duration.value;
                        
                        window.value = duration;
                        
                        priceperMin = 0.25;
                        document.getElementById("time").innerHTML = "Estimated Time: " + duration;
                        document.getElementById("cost").innerHTML = "Estimated Price: £" + ((element.duration.value/60) * priceperMin).toFixed(2);
                    }
                }
            }

        }




    </script>
    <!--Load the API from the specified URL
    * The async attribute allows the browser to render the page while the API loads
    * The key parameter will contain your own API key (which is not needed for this tutorial)
    * The callback parameter executes the initMap() function
    -->
    <script async defer
            src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAq7skhdNDoB5ZTzrRMosH3bvys5LVxgyk&callback=initMap">
    </script>

    <div id="activeRide" class="modal" runat="server">
        <div class ="modal-content">
            <h1> <b>You have an active ride.</b></h1>
            <h2>Would you like to continue?</h2>
            <i>Note: Pressing No will cancel the active ride</i>
            <button id="activenoBtn">No</button>
            <button id="activeyesBtn">Yes</button>
        </div>

    </div>

    <div class="request">

        <div id="driverView" runat="server">

        </div>
        <br /><br />

    </div>

 
    <script>
         document.getElementById("yesBtn").onclick = function () {
             document.getElementById('moreRiders').style.display = 'none';
        }

        
        

      

        var modal = document.getElementById("myModal");

        // Get the button that opens the modal
        var btn = document.getElementById("requestBtn");

        // When the user clicks the button, open the modal
        

            modal.style.display = "block";


        // Get the <span> element that closes the modal
        var span = document.getElementsByClassName("close")[0];

        var ridemodal = document.getElementById("rideRequest");

        // Get the button that opens the modal
        var ridebtn = document.getElementById("rideClose");

        var accept = document.getElementById("acceptRideBtn");

    

       


        // When the user clicks on <span> (x), close the modal
        span.onclick = function () {
            modal.style.display = "none";
        }

        // When the user clicks anywhere outside of the modal, close it
        window.onclick = function (event) {
            if (event.target == modal) {
                modal.style.display = "none";
            }
            if (event.target == ridemodal) {
                ridemodal.style.display = "none";
            }
        }

        document.getElementById("changeBtn").onclick = function () {
            document.getElementById('destinationModal').style.display = 'block';
        }




       
       
    </script>
    <form id="riderMarkers" runat="server">

    </form>
    </body>


</html>
