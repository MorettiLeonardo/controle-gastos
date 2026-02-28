import axios from "axios";
const urlBase = import.meta.env.VITE_BASE_URL;
const api = axios.create({
    baseURL: urlBase,
    headers: {
        "Content-Type": "application/json",
        Accept: "*/*",
    },
});

export default api;