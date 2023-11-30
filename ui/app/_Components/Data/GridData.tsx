import axios from 'axios'
import https from 'https'
import useCurrentDate from '../Hooks/useCurrentDate'
import { DateProp } from '../Interfaces/Interfaces'

const getData = async ({ dateRange, startDate, endDate }: DateProp) => {
   const [start, end] = useCurrentDate()
   const startParam = `?startDate=${startDate ?? start}`
   const endParam = `&endDate=${endDate ?? end}`

   const agent = new https.Agent({
      rejectUnauthorized: false,
   })

   const api = axios.create({
      baseURL: process.env.GRID_BASE_URL,
      httpsAgent: agent,
   })

   try {
      const response = await api.get(dateRange + startParam + endParam)
      return response.data
   } catch (error) {
      console.error(error)
   }
}

export default getData

export const revalidate = 30
