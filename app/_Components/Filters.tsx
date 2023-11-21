'use client'
import Date from './Date'
import { useEffect, useState } from 'react'

const Filters = () => {
   const [selectedDate, setSelectedDate] = useState()
   const [date, setDate] = useState()

   useEffect(() => {
      // setDate()
   })

   function handleDateChange() {
      console.log('HI')
   }

   return (
      <div className="flex gap-12 justify-around items-center px-28">
         <Date onDateChange={() => handleDateChange} />
         <div className="flex w-96 gap-4">
            Interval Aggregation:
            <label>
               <input
                  type="radio"
                  name="filter"
                  value="NETYPE"
                  className="mr-1 align-middle"
               />
               Hourly
            </label>
            <label>
               <input
                  type="radio"
                  name="filter"
                  value="NETYPE"
                  className="mr-1 align-middle"
               />
               Daily
            </label>
         </div>
         <div className="flex">
            <p className="whitespace-nowrap mr-2">KPIs:</p>
            <p className="whitespace-nowrap underline">3 items selected</p>
         </div>
         <div className="flex w-96 gap-4">
            Grouping:
            <label>
               <input
                  type="radio"
                  name="filter"
                  value="NETYPE"
                  className="mr-1 align-middle"
               />
               OFF
            </label>
            <label>
               <input
                  type="radio"
                  name="filter"
                  value="NETYPE"
                  className="mr-1 align-middle"
               />
               NETYPE
            </label>
            <label>
               <input
                  type="radio"
                  name="filter"
                  value="NEALIAS"
                  className="mr-1 align-middle"
               />
               NEALIAS
            </label>
         </div>
      </div>
   )
}

export default Filters
