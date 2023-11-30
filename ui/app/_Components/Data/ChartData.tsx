import axios from 'axios'
import https from 'https'

const getData = async () => {
   const agent = new https.Agent({
      rejectUnauthorized: false,
   })

   const api = axios.create({
      baseURL: process.env.CHART_BASE_URL,
      httpsAgent: agent,
   })

   try {
      const response = await api.get('/')
      return response.data
   } catch (error) {
      console.error(error)
   }
}

export default getData

export const revalidate = 30
