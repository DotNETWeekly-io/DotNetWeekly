import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
    vus: 10,
    duration: '1m',
    insecureSkipTLSVerify: true
}

export default function () {
    const url = "https://localhost:7200/api/customer";
    const payload = JSON.stringify({
        Name: "fenga",
        Email: "",
    });

    const params = {
        headers: {
            'Content-Type': 'application/json',
        },
    };

    const res = http.post(url, payload, params);
    check(res, {
        'is status 400': (r) => r.status === 400,
    });
}