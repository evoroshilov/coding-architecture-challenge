export class SettingsService { 

  getApiUrl(apiMethod: string) {
    //load configuration from server
    const baseUrl = 'https://localhost:49159/'
    return baseUrl + "/api/v1" + apiMethod;
  }
}