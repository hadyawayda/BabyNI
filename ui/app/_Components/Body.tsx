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
   const [grouping, setGrouping] = useState<string>('All')
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

   function onIntervalChange(dateInterval: ReactChange) {
      setInterval(dateInterval.target.value)
   }

   function onDateChange(date: DateRange) {
      setDateRange(date)
   }

   async function fetchNewData() {
      setData(await callData(interval, dateRange))
   }

   function onDateTimeKeyChange(selection: ReactChange) {
      const { name, checked } = selection.target

      setDateTimeKeys((prev) => ({
         ...prev,
         [name]: checked,
      }))
   }

   function onKPISelect(KPI: ReactChange) {
      const { name, checked } = KPI.target
      const filteredMap = new Map(selectedKPIs)
      setSelectedKPIs(filteredMap.set(name, checked))
   }

   // function fetchDateTimeKeys() {
   //    let map = new Map<number, boolean>()

   //    data.forEach((x) => {
   //       map.set(x.DATETIME_KEY, true)
   //    })

   // console.log(map)
   // }

   function onGroupingChange(e: ReactChange) {
      setGrouping(e.target.value)
   }

   useEffect(() => {
      fetchNewData()
      // fetchDateTimeKeys()
   }, [interval, dateRange])

   // useEffect(() => {
   //    fetchDateTimeKeys()
   // }, [])

   // useEffect(() => console.log() ,[])

   return (
      <>
         <div className="bg-white text-black h-full w-full rounded-l-md my-4 ml-4 pt-6 ">
            <Suspense>
               <Filters
                  onDateChange={onDateChange}
                  onIntervalChange={onIntervalChange}
                  onKPISelect={onKPISelect}
                  onGroupingChange={onGroupingChange}
                  {...{
                     selectedKPIs,
                     interval,
                     dateTimeKeys,
                     grouping,
                     onDateTimeKeyChange,
                  }}
               />
            </Suspense>
            <div className="flex flex-col items-center justify-center overflow-y-scroll">
               <Suspense>
                  <Grid {...{ data, selectedKPIs, grouping, dateTimeKeys }} />
               </Suspense>
               <Suspense>
                  <Chart
                     {...{
                        data,
                        selectedKPIs,
                        grouping,
                        dateTimeKeys,
                        onDateTimeKeyChange,
                     }}
                  />
               </Suspense>
            </div>
         </div>
      </>
   )
}

export default Body

// new Map([
//    [2072264378, true],
//    [2072262378, true],
//    [2072234378, true],
//    [2072264578, true],
// ])
