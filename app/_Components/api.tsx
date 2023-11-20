import axios from 'axios'
import https from 'https'
import useCurrentDate from './useCurrentDate'

const getData = async (
   dateRange: string,
   startDate?: string,
   endDate?: string
) => {
   let [start, end] = useCurrentDate()

   const startParam = `?startDate=${startDate ?? start}`

   const endParam = `&endDate=${endDate ?? end}`

   const agent = new https.Agent({
      rejectUnauthorized: false,
   })

   const api = axios.create({
      baseURL: process.env.BASE_URL,
      httpsAgent: agent,
   })

   const response = await api.get(dateRange + startParam + endParam)

   return response.data
}

export default getData

export const revalidate = 30
