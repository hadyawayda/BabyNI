'use client'

import Grid from './Grid'
import Chart from './Chart'
import Filters from './Filters'
import { Suspense, useState } from 'react'
import { DataProps, gridProps, ReactEvent } from './Interfaces/Interfaces'
import { callData } from './Data/CallData'

const Body = ({ gridData, chartData }: DataProps) => {
   const KPIs = {
      RSL_INPUT_POWER: true,
      MAX_RX_LEVEL: true,
      RSL_DEVIATION: true,
   }
   const [data, setData] = useState<gridProps>(gridData)
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
                     props={chartData}
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
