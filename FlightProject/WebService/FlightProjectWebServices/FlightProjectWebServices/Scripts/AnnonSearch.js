function onSearch(){
    $tableFlights=("$flightsTable")
    $byFlight=("$flightid")
    $byOrigin=("$originoption")
    $byDest=("$destoption")
    $byAirline=("$airlinename")
    $.ajax({
        url=`api/search/$/{airline}/{origin}/{dest}/{depDate}/{LandDate}/{vacancy}`
    })
}