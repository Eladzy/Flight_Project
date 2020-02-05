const flights = [];
jQuery(document).ready(function () {
    console.log("loading table")
    $tableFlights = $("#flightsTable")
    $.ajax({
        url: "/api/flights"
    }).then(fillTable()).catch((err) => { console.log(err) })
})
function fillTable() {
    $.each(flights, (i, flight) => {
        flight = new Flight(flight.Id, flight.AirLine_Id, flight.Origin_Country_Code, flight.Destination_Country_Code, flight.Landing_Time, flight.Departure_Time, undefined)
        console.log(flight + "\n +++debug " + typeof (flight) + " +++debug")

        $tableFlights.append("<tr>" +

            "<td>" + flight.AirLine_Id + "</td>" +
            "<td>" + flight.Origin_Country_Code + "</td>" +
            "<td>" + flight.Destination_Country_Code + "</td>" +
            "<td>" + flight.Departure_Time + "</td>" +
            "<td>" + flight.Landing_Time + "</td>" +
            "<td>" + flight.status + "</td>" +
            "</tr>")
    })       
};


function onSearch(){
    const flight_id = $("#flightid").val()
    const air_company_name = $("#airlineSelect").val()// fetch
    const origin_country = $("#originoption").val()
    const dest_country = $("#destoption").val()
    const dep_time = $("#deptime").val()
    const land_time = $("#landtime").val()
    $tableFlights=("#flightsTable")
    $.ajax({
        //url:`api/search/${air_company_name}/`
        url: `api/search?`,//${(air_company_name != null?'origin='+origin_country+'&':'' )}`
        type: "GET",
        data: 
            { "flightID": flight_id, "airlineName": air_company_name, "originCountry": origin_country, "destCountry": dest_country, "depTime": dep_time, "landTime": land_time },
        success: fillTable(flights).catch((err) => { console.log(err) })  
    })
}