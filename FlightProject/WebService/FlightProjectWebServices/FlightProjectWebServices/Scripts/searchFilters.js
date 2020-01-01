const flights=[]
function onSearch(){
    const flight_id = $("#flightid").val()
    const air_company_name = $("#airlineSelect").val()// fetch
    const origin_country = $("#originoption").val()
    const dest_country = $("#destoption").val()
    const dep_time = $("#deptime").val()
    const land_time = $("#landtime")
    $tableFlights=("#flightsTable")
    $.ajax({
        //url:`api/search/${air_company_name}/`
        url:`api/search?`,//${(air_company_name != null?'origin='+origin_country+'&':'' )}`
        data: 
        { "flightID" : flight_id,"airlineName" : air_company_name,"originCountry" :origin_country,"destCountry": dest_country,"depTime": dep_time,"landTime": land_time },
        type: "GET"
        
    
    })
}