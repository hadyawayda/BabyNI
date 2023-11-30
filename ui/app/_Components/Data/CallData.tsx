import https from 'https'
import axios from 'axios'
import { DateRange } from '../Interfaces/Interfaces'

export async function callData(interval: string, { start, end }: DateRange) {
   const startParam = `?startDate=${start}`
   const endParam = `&endDate=${end}`

   const agent = new https.Agent({
      rejectUnauthorized: false,
   })

   const api = axios.create({
      baseURL: process.env.NEXT_PUBLIC_GRID_BASE_URL,
      httpsAgent: agent,
   })

   try {
      const response = await api.get(interval + startParam + endParam)
      return response.data
   } catch (error) {
      console.error(error)
   }
}
