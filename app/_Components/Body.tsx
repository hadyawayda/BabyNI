'use client'

import Grid from './Grid'
import Chart from './Chart'
import Filters from './Filters'
import { Suspense, useEffect, useState } from 'react'
import { GridProps, Props, ReactEvent } from '../Interfaces/Interfaces'
import { callData } from './CallData'

const Body = ({ props }: GridProps) => {
   const KPIs = {
      RSL_INPUT_POWER: true,
      MAX_RX_LEVEL: true,
      RSL_DEVIATION: true,
   }
   const [data, setData] = useState<Props>(props)
   const [grouping, setGrouping] = useState<string>('Both')
   const [selectedKPIs, setSelectedKPIs] = useState<object>(KPIs)

   async function handleDataFetch(interval: string) {
      setData(await callData(interval))
   }

   function handleDateChange() {}

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

   useEffect(() => {
      console.log(selectedKPIs)
   }, [selectedKPIs])

   return (
      <>
         <div className="bg-white text-black h-full w-full rounded-l-md my-4 ml-4 pt-6 ">
            <Suspense>
               <Filters
                  onDateChange={handleDateChange}
                  onIntervalChange={handleDataFetch}
                  onKPIChange={handleKPIChange}
                  onGroupingChange={handleGroupingChange}
                  selectedKPIs={selectedKPIs}
               />
            </Suspense>
            <div className="flex flex-col items-center justify-center overflow-y-scroll">
               <Suspense>
                  <Grid props={data} />
               </Suspense>
               <Suspense>
                  <Chart
                     props={data}
                     grouping={grouping}
                     selectedKPIs={selectedKPIs}
                  />
               </Suspense>
            </div>
         </div>
      </>
   )
}

export default Body
