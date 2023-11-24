import https from 'https'
import axios from 'axios'

export async function callData(interval: string) {
   const agent = new https.Agent({
      rejectUnauthorized: false,
   })

   const api = axios.create({
      baseURL: process.env.NEXT_PUBLIC_BASE_URL,
      httpsAgent: agent,
   })

   try {
      const response = await api.get(interval)
      return response.data
   } catch (error) {
      console.error(error)
   }
}
