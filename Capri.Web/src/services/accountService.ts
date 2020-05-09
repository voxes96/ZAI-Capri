import { requestService } from '@src/services/requestService'

class AccountService {

    public login(email: string, password: string) {
        return requestService.request("POST", "/account/login", {
            email: email,
            password: password
        }).then((response: any) => 
        {
            console.log(response.data)
            sessionStorage.setItem('token', response.data.securityStamp)
        })
    }
}

export const accountService = new AccountService();