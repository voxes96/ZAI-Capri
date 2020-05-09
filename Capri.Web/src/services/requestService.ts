import { Vue } from 'vue-property-decorator';
import { Method } from 'axios';

class RequestService {
    // private serverAddres: string = 'http://localhost';
    private serverAddres: string = 'http://capriweb.azurewebsites.net';
    public async request(method: string, api: string, datas = {}) {
        const token = sessionStorage.token;
        return Vue.axios.request({
            url: this.serverAddres + api,
            method: method as Method,
            data: datas,
            headers: {
                Authorization: 'Bearer ' + token,
            },
        })
        .catch(function (error) {
            console.log(error);
        });
    }

    public async requestFile(method: string, api: string, datas = {}) {
        const token = sessionStorage.token;
        return Vue.axios.request({
            url: this.serverAddres + api,
            method: method as Method,
            data: datas,
            responseType: 'blob',
            headers: {
                Authorization: 'Bearer ' + token,
            },
        })
        .catch(function (error) {
            console.log(error);
        });
    }
}

export const requestService = new RequestService();