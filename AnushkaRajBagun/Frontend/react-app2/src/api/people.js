import axios from "axios";

//create takes an object and inside this object we define our base url of the server
export default axios.create({
  baseURL: "http://localhost:5000/api/",
});
