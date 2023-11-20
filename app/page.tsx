// 'use client'
import Grid from './_Components/Grid'
import Chart from './_Components/Chart'
import getData from './_Components/API'
import Filters from './_Components/Filters'
import { Suspense } from 'react'

export default async function App() {
   const dailyData = await getData('hourly')

   return (
      <div className="bg-white text-black h-full w-full rounded-l-md my-4 ml-4 pt-6 ">
         <Suspense>
            <Filters />
         </Suspense>
         <div className="flex flex-col items-center justify-center overflow-y-scroll">
            <Suspense>
               <Chart props={dailyData} />
            </Suspense>
            <Suspense>
               <Grid props={dailyData} />
            </Suspense>
         </div>
      </div>
   )
}
