'use client'

import Grid from './Grid'
import Chart from './Chart'
import Filters from './Filters'
import { Suspense, useEffect, useState } from 'react'
import {
   DataProps,
   DateRange,
   gridProps,
   ReactChange,
} from './Interfaces/Interfaces'
import { callData } from './Data/CallData'
import useDateString from './Hooks/useDateString'

const Body = ({ gridData, chartData }: DataProps) => {
   const [data, setData] = useState<gridProps>(gridData)
   const [interval, setInterval] = useState<string>('daily')
   const [selectedKPIs, setSelectedKPIs] = useState<Map<string, boolean>>(
      new Map([
         ['RSL_INPUT_POWER', true],
         ['MAX_RX_LEVEL', true],
         ['RSL_DEVIATION', true],
      ])
   )
   const [grouping, setGrouping] = useState<string>('NETYPE')
   const [dateTimeKeys, setDateTimeKeys] = useState<object>({
      '2072264378': true,
      '2072262378': true,
      '2072234378': true,
      '2072264578': true,
   })
   const [dateRange, setDateRange] = useState<DateRange>(
      useDateString({
         start: new Date(new Date().setDate(new Date().getDate() - 30)),
         end: new Date(),
      })
   )

   function handleDataFetch(dateInterval: ReactChange) {
      setInterval(dateInterval.target.value)
   }

   function handleDateChange(date: DateRange) {
      setDateRange(date)
   }

   async function fetchNewData() {
      setData(await callData(interval, dateRange))
   }

   function fetchDateTimeKeys() {
      let map = new Map<number, boolean>()

      data.forEach((x) => {
         map.set(x.DATETIME_KEY, true)
      })

      // console.log(map)
   }

   useEffect(() => {
      fetchNewData()
      fetchDateTimeKeys()
   }, [interval, dateRange])

   useEffect(() => {
      fetchDateTimeKeys()
   }, [])

   function handleKPIChange(KPI: ReactChange) {
      const { name, checked } = KPI.target
      const filteredMap = new Map(selectedKPIs)
      filteredMap.set(name, checked)
      setSelectedKPIs(filteredMap)
      // setSelectedKPIs((prev) => ({
      //    ...prev,
      //    [name]: checked,
      // }))
   }

   useEffect(() => console.log(selectedKPIs), [selectedKPIs])

   function handleGroupingChange(e: ReactChange) {
      setGrouping(e.target.value)
   }

   function handleDateTimeKeyChange(selection: ReactChange) {
      const { name, checked } = selection.target

      setDateTimeKeys((prev) => ({
         ...prev,
         [name]: checked,
      }))
   }

   return (
      <>
         <div className="bg-white text-black h-full w-full rounded-l-md my-4 ml-4 pt-6 ">
            <Suspense>
               <Filters
                  onDateChange={handleDateChange}
                  onIntervalChange={handleDataFetch}
                  onKPISelect={handleKPIChange}
                  onGroupingChange={handleGroupingChange}
                  onDateTimeKeySelect={handleDateTimeKeyChange}
                  {...{ selectedKPIs, interval, dateTimeKeys, grouping }}
               />
            </Suspense>
            <div className="flex flex-col items-center justify-center overflow-y-scroll">
               <Suspense>
                  <Grid {...{ data, selectedKPIs, grouping, dateTimeKeys }} />
               </Suspense>
               <Suspense>
                  <Chart {...{ data, selectedKPIs, grouping }} />
               </Suspense>
            </div>
         </div>
      </>
   )
}

export default Body
