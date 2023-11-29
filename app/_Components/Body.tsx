'use client'

import Grid from './Grid'
import Chart from './Chart'
import Filters from './Filters'
import { Suspense, useEffect, useState } from 'react'
import {
   DataProps,
   DateRange,
   gridProps,
   ReactEvent,
} from './Interfaces/Interfaces'
import { callData } from './Data/CallData'
import useDateString from './Hooks/useDateString'

const Body = ({ gridData, chartData }: DataProps) => {
   const KPIs = {
      RSL_INPUT_POWER: true,
      MAX_RX_LEVEL: true,
      RSL_DEVIATION: true,
   }

   const [data, setData] = useState<gridProps>(gridData)
   const [interval, setInterval] = useState<string>('daily')
   const [selectedKPIs, setSelectedKPIs] = useState<object>(KPIs)
   const [grouping, setGrouping] = useState<string>('NETYPE')
   const [dateTimeKeys, setDateTimeKeys] = useState<object>({
      2072264378: true,
      2072262378: true,
      2072234378: true,
      2072264578: true,
   })
   const [selectedDateTimeKeys, setSelectedDateTimeKeys] = useState<number[]>([
      2072264378,
   ])
   const [dateRange, setDateRange] = useState<DateRange>(
      useDateString({
         start: new Date(new Date().setDate(new Date().getDate() - 30)),
         end: new Date(),
      })
   )

   function handleDataFetch(dateInterval: string) {
      setInterval(dateInterval)
   }

   function handleDateChange(date: DateRange) {
      setDateRange(date)
   }

   async function fetchNewData() {
      setData(await callData(interval, dateRange))
   }

   function fetchDateTimeKeys() {
      // const updatedArray = [...dateTimeKeys!]
      // gridData.forEach((x) => {
      //    if (!dateTimeKeys!.includes(x.DATETIME_KEY)) {
      //       updatedArray.push(x.DATETIME_KEY)
      //    }
      // })
      // setDateTimeKeys(updatedArray)
   }

   useEffect(() => {
      fetchNewData()
      fetchDateTimeKeys()
   }, [interval, dateRange])

   useEffect(() => {
      fetchDateTimeKeys()
   }, [])

   function handleKPIChange(KPI: ReactEvent) {
      const { name, checked } = KPI

      setSelectedKPIs((prev) => ({
         ...prev,
         [name]: checked,
      }))
   }

   function handleGroupingChange(e: ReactEvent) {
      setGrouping(e.value)
   }

   return (
      <>
         <div className="bg-white text-black h-full w-full rounded-l-md my-4 ml-4 pt-6 ">
            <Suspense>
               <Filters
                  onDateChange={handleDateChange}
                  onIntervalChange={handleDataFetch}
                  onKPIChange={handleKPIChange}
                  onGroupingChange={handleGroupingChange}
                  {...{ selectedKPIs, interval, dateTimeKeys, grouping }}
               />
            </Suspense>
            <div className="flex flex-col items-center justify-center overflow-y-scroll">
               <Suspense>
                  {/*
                     Add DateTime_Key selector
                  */}
                  <Grid
                     {...{ data, selectedKPIs, grouping, selectedDateTimeKeys }}
                  />
               </Suspense>
               <Suspense>
                  <Chart {...{ chartData, grouping, selectedKPIs }} />
               </Suspense>
            </div>
         </div>
      </>
   )
}

export default Body
