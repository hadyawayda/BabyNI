'use client'
import Date from './Date'
import { useEffect, useState } from 'react'
import KPISelector from './KPISelector'

const Filters = ({
   onDateChange,
   onIntervalChange,
   onKPIChange,
}: {
   onDateChange: () => void
   onIntervalChange: (interval: string) => void
   onKPIChange: (KPIs: EventTarget & HTMLInputElement) => void
}) => {
   function handleDateChange() {
      onDateChange()
   }

   function handleIntervalChange(e: React.ChangeEvent<HTMLInputElement>) {
      onIntervalChange(e.target.value)
   }

   function handleKPIChange(KPI: EventTarget & HTMLInputElement) {
      onKPIChange(KPI)
   }

   return (
      <div className="flex gap-4 justify-around items-center px-28">
         <Date onDateChange={() => handleDateChange} />
         <div className="flex justify-center w-96 gap-2">
            Interval Aggregation:
            <label>
               <input
                  type="radio"
                  name="filter"
                  value="hourly"
                  className="mr-1 align-middle"
                  onChange={handleIntervalChange}
               />
               Hourly
            </label>
            <label>
               <input
                  type="radio"
                  name="filter"
                  value="daily"
                  className="mr-1 align-middle"
                  onChange={handleIntervalChange}
               />
               Daily
            </label>
         </div>
         <div className="flex">
            <p className="whitespace-nowrap mr-2">KPIs:</p>
            <KPISelector onKPISelect={handleKPIChange} />
         </div>
         <div className="flex justify-center w-96 gap-2 ml-4">
            Grouping:
            <label>
               <input
                  type="radio"
                  name="filter"
                  value="NETYPE"
                  className="whitespace-nowrap mr-1 align-middle"
               />
               OFF
            </label>
            <label>
               <input
                  type="radio"
                  name="filter"
                  value="NETYPE"
                  className="whitespace-nowrap mr-1 align-middle"
               />
               NETYPE
            </label>
            <label>
               <input
                  type="radio"
                  name="filter"
                  value="NEALIAS"
                  className="whitespace-nowrap mr-1 align-middle"
               />
               NEALIAS
            </label>
         </div>
      </div>
   )
}

export default Filters
