// 'use client'

import axios from 'axios'
import https from 'https'
// import { useContext } from 'react'
// import { SharedContext } from './Filters'
// const { Grouping } = useContext(SharedContext)

const getHourlyData = async () => {
   const url = 'https://localhost:7096/api/hourly'

   const agent = new https.Agent({
      rejectUnauthorized: false,
   })

   const response = await axios.get(url, { httpsAgent: agent })

   return response.data
}

export const getDailyData = async () => {
   const url = 'https://localhost:7096/api/daily'

   const agent = new https.Agent({
      rejectUnauthorized: false,
   })

   const response = await axios.get(url, { httpsAgent: agent })

   return response.data
}

export const getHourlyDataWithDate = async (
   startDate: string,
   endDate: string
) => {
   const url = `https://localhost:7096/api/hourly?startDate=${startDate}&endDate=${endDate}`

   const agent = new https.Agent({
      rejectUnauthorized: false,
   })

   const response = await axios.get(url, { httpsAgent: agent })

   return response.data
}

export const getDailyDataWithDate = async (
   startDate: string,
   endDate: string
) => {
   const url = `https://localhost:7096/api/daily?startDate=${startDate}&endDate=${endDate}`

   const agent = new https.Agent({
      rejectUnauthorized: false,
   })

   const response = await axios.get(url, { httpsAgent: agent })

   return response.data
}

export default getHourlyData

export const revalidate = 30
