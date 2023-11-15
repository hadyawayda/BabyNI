import axios from 'axios'
import https from 'https'

const Data = async () => {
   const url = 'https://localhost:7096/api/daily'

   const agent = new https.Agent({
      rejectUnauthorized: false,
   })

   const response = await axios.get(url, { httpsAgent: agent })

   return await response.data
}

export default Data
