const flights={}
function onSearch(){
    const air_company_name = ("#airlineSelect").val()// fetch
    const origin_country=("#originoption").val()
    const dest_country=("#destoption").val()
    $tableFlights=("#flightsTable")
    $.ajax({
        //url:`api/search/${air_company_name}/`
        url:`api/search?${(air_company_name != null?'origin='+origin_country+'&':'' )}`
    
    })
}