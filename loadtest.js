import http from 'k6/http';
import { sleep } from 'k6';

const baseUrl = 'http://localhost:8080/api/HackerNews/best20';

export let options = {
    vus: 1000,
    duration: '60s',
    thresholds: {
        http_req_duration: ['p(95)<1500']
    }
};

export default function () {
    http.get(`${baseUrl}`);
    sleep(1);
}