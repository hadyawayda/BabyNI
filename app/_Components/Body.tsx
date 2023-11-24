'use client'

import https from 'https'
import Grid from './Grid'
import Chart from './Chart'
import Filters from './Filters'
import { Suspense, useEffect, useState } from 'react'
import { Data, GridProps } from '../Interfaces/Interfaces'
import axios from 'axios'

const Body = ({ props }: GridProps) => {
   const [data, setData] = useState<Data[]>(props)
   const [selectedKPIs, setSelectedKPIs] = useState({
      RSL_INPUT_POWER: true,
      MAX_RX_LEVEL: true,
      RSL_DEVIATION: true,
   })

   async function callData(interval: string) {
      const agent = new https.Agent({
         rejectUnauthorized: false,
      })

      const api = axios.create({
         baseURL: 'https://localhost:7096/api/',
         httpsAgent: agent,
      })

      try {
         const response = await api.get(interval)
         setData(response.data)
      } catch (error) {
         console.error(error)
      }
   }

   function handleDateChange() {}

   function handleKPIChange(KPI: EventTarget & HTMLInputElement) {
      const { name, checked } = KPI
      setSelectedKPIs((prev) => ({
         ...prev,
         [name]: checked,
      }))
   }

   useEffect(() => {
      console.log(selectedKPIs)
   }, [selectedKPIs])

   return (
      <>
         <div className="bg-white text-black h-full w-full rounded-l-md my-4 ml-4 pt-6 ">
            <Suspense>
               <Filters
                  onDateChange={handleDateChange}
                  onIntervalChange={callData}
                  onKPIChange={handleKPIChange}
                  selectedKPIs={selectedKPIs}
               />
            </Suspense>
            <div className="flex flex-col items-center justify-center overflow-y-scroll">
               <Suspense>
                  <Grid props={data} />
               </Suspense>
               <Suspense>
                  <Chart props={data} />
               </Suspense>
            </div>
         </div>
      </>
   )
}

export default Body
