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
      <div className="flex gap-12 justify-center items-center">
         <Date onDateChange={() => handleDateChange} />
         <div>KPIs Filter</div>
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
