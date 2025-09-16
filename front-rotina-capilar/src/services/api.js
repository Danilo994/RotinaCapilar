import axios from "axios";

const api = axios.create({
    baseUrl: "https://localhost:7126/api",
});

export default api;