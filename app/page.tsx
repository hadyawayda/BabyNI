// 'use client'
import GridComponent from './_Components/Grid'
import ChartComponent from './_Components/Chart'
import getData from './_Components/API'
import Filters from './_Components/Filters'
import { Suspense } from 'react'

export default async function App() {
   const dailyData = await getData('hourly')

   return (
      <div className="bg-white text-black h-full rounded-l-md my-4 ml-4 py-4 pl-5 pr-px">
         <Suspense>
            <Filters />
         </Suspense>
         <div className="flex justify-center">
            <Suspense>{/* <ChartComponent props={dailyData} /> */}</Suspense>
            <Suspense>
               <GridComponent props={dailyData} />
            </Suspense>
         </div>
      </div>
   )
}
