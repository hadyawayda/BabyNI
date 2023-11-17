import axios from 'axios'
import https from 'https'

const getDailyData = async () => {
   const url = 'https://localhost:7096/api/daily'

   const agent = new https.Agent({
      rejectUnauthorized: false,
   })

   const response = await axios.get(url, { httpsAgent: agent })

   return response.data
}

export const getHourlyData = async () => {
   const url = 'https://localhost:7096/api/hourly'

   const agent = new https.Agent({
      rejectUnauthorized: false,
   })

   const response = await axios.get(url, { httpsAgent: agent })

   return response.data
}

export default getDailyData

export const revalidate = 30
