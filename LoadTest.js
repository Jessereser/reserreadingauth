import http from 'k6/http';
import {sleep, check} from 'k6';

//shift rightclick en open in de map van de test een powershell.
//om vervolgens de test te runnen via K6 typ: "k6 run test.js"


export let options = {
    // LoadTest
    insecureSkipTLSVerify: true,
    noConnectionReuse: false,
    stages:[
        {duration : '30s', target : 150},
        {duration : '2m', target : 150},
        {duration : '2s', target : 0},
    ],
    tresholds:{
        http_req_duration:['p(99)<200'],
        http_req_failed:['rate<0.01'],
    }
}

const base_url = 'https://localhost:5001'
// Een getter zonder enige parameters.
export default () =>{
    const responses = http.batch([
        ['GET', `${base_url}/api/account/all`],
    ])
    check(responses[0], {
        'status is 200': res => res.status === 200
    })
    sleep(1)
}