export interface WeatherAlertGetDto {
    weatherAlertId: number;
    title:          string;
    body:           string;
    imgPath:        string;
    url:            string;
    modifiedAt:     string;
    createdAt:      string;
    status:         boolean;
}