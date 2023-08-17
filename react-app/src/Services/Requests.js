import axios from "axios";

export default class Requests {

    getMessages = async function(tags = null) {
        try {
            const tagsTail = tags && tags.length > 0 ? '?tags=' + tags.join('&tags=') : '';
            const result = await axios.get(process.env.REACT_APP_API_BASE_URL + "/api/message" + tagsTail)
            return result.data;
        } catch (error) {
            console.log(error)
        }

        return [];
    }

    getTags  = async function(startWith) {
        try {
            const result = await axios.get(process.env.REACT_APP_API_BASE_URL + "/api/tag" + (startWith ? "?startWith=" + startWith : ''))
            return result.data;
        } catch (error) {
              console.log(error)
        }

        return [];
    }
}