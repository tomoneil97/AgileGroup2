<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="_Default" %>

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
        }
    </style>
    <meta name="viewport" content="width=device-width, initial-scale=1">
</head>

<body onload="document.getElementById('destinationModal').style.display='block'">
    <div>
        <nav class="topbar">
            
            <input type="image" src="<%=img %>" alt="icon" class="userIcon" onclick="openNav()">

             <a href="#" class="notification">
                <span>Inbox</span>
                 <div id="notifDiv" runat="server">
                     <span class="badge"><% =notifNum %></span>
                 </div>
            </a> 
        </nav>
    </div>
    

    <div id="mySidenav" class="sidenav">
        <br />
        <a href="Home.aspx">Home</a>
        <a href="ProfilePage.aspx">Profile</a>
        <a href="MyRides.aspx">My Rides</a>
        <a asp-page="./Settings">Settings</a>
        <a asp-page="./Privacy">Privacy</a>
        <a asp-page="./Contact">Contact</a>

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
    <div id="map"></div>
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

            // Try HTML5 geolocation.
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(function (position) {
                    pos = {
                        lat: position.coords.latitude,
                        lng: position.coords.longitude
                    };

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



    <div class="request">
        <div id="riderView" runat="server">
            <label id="time">Estimated Time: </label>
        <br />
        <label id="cost">Estimated Price: </label>

        <button id="requestBtn" class="request"><b>Request Ride</b></button>
        <br />
        <img src="images/powered_by_google_on_non_white.png" />
        </div>
        

    </div>

    <div id="destinationModal" class="modal">
        <div class ="modal-content">
            <h1> Select a location</h1>
            <select id="destination">
                <option value="53.7696187, -0.3682079">Front Entrance</option>
                <option value="53.7700431, -0.3653883">Salmon Grove</option>
                <option value="53.771319, -0.3726109">Westfield Court</option>
                <option value="53.7739858, -0.3685683">Sport Centre</option>
            </select>
            <button id="closeBtn">Okay</button>
        </div>

    </div>

    <div id="myModal" class="modal">
        <div class="modal-content">
            <span class="close">&times;</span>
            <p>Ride Requested!</p>
            <p> Your destination is:</p>
            <br />
            <label id="arrival"> Arriving by: </label>

        </div>
    </div>

    <div id="noGPS" class="modal">
        <div class="modal-content">
            <span class="close">&times;</span>
            <p>This site requires Geolocation!</p>
            <p> Please enable Location services</p>
            <br />
           

        </div>
    </div>


    <script>
        var modal = document.getElementById("myModal");

        // Get the button that opens the modal
        var btn = document.getElementById("requestBtn");

        // Get the <span> element that closes the modal
        var span = document.getElementsByClassName("close")[0];


        


        // When the user clicks the button, open the modal
        btn.onclick = function () {
           // GetArrivalTime();
            
            var currentDate = new Date();
            var arrivalTime = new Date(currentDate.getTime() + (durationValue * 1000));
            document.getElementById("arrival").innerHTML = "Arriving by: " + arrivalTime;
            modal.style.display = "block";
        }

        // When the user clicks on <span> (x), close the modal
        span.onclick = function () {
            modal.style.display = "none";
        }

        // When the user clicks anywhere outside of the modal, close it
        window.onclick = function (event) {
            if (event.target == modal) {
                modal.style.display = "none";
            }
        }



    </script>
</body>


</html>
