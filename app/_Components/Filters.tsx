'use client'

import { createContext } from 'react'

export const SharedContext = createContext({
   Grouping: 'OFF',
})

const Filters = () => {
   let filter = ''

   const handleRadioChange = (event: string) => {
      filter = event
   }

   const grouping = {
      Grouping: 'OFF',
   }

   return (
      <div className="flex gap-12">
         <div>Date Filter</div>
         <div>KPIs Filter</div>
         <SharedContext.Provider value={grouping}>
            <div className="flex w-96 gap-4">
               Grouping:
               <label>
                  <input
                     type="radio"
                     name="filter"
                     value="NETYPE"
                     checked={filter === 'NETYPE'}
                     className="mr-1"
                  />
                  OFF
               </label>
               <label>
                  <input
                     type="radio"
                     name="filter"
                     value="NETYPE"
                     checked={filter === 'NETYPE'}
                     className="mr-1"
                  />
                  NETYPE
               </label>
               <label>
                  <input
                     type="radio"
                     name="filter"
                     value="NEALIAS"
                     checked={filter === 'NEALIAS'}
                     className="mr-1"
                  />
                  NEALIAS
               </label>
            </div>
         </SharedContext.Provider>
      </div>
   )
}

export default Filters
