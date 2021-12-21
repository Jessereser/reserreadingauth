import http from 'k6/http';
import {sleep, check} from 'k6';

//shift rightclick en open in de map van de test een powershell.
//om vervolgens de test te runnen via K6 typ: "k6 run test.js"

export let options = {
    // StressTest. Deze valt niet te halen en is om te checken hoeveel mijn applicatie aankan
    insecureSkipTLSVerify: true,
    noConnectionReuse: false,
    stages: [
        {duration: '5s', target: 100},
        {duration: '10s', target: 100},
        {duration: '5s', target: 200},
        {duration: '10s', target: 200},
        {duration: '5s', target: 300},
        {duration: '10s', target: 300},
        {duration: '5s', target: 400},
        {duration: '10s', target: 400},
        {duration: '20s', target: 0},
    ],
    tresholds: {
        
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