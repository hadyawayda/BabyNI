// 'use client'
import GridComponent from './_Components/Grid'
import Chart from './_Components/Chart'
import getHourlyData, { getDailyData } from './_Components/API'
import Filters from './_Components/Filters'

export default async function App() {
   const dailyData = await getDailyData()
   const hourlyData = await getHourlyData()

   return (
      <div className="bg-white text-black h-full rounded-l-md my-4 ml-4 py-4 pl-5 pr-px">
         <Filters />
         <div>
            {/* <Chart props={data} /> */}
            <GridComponent props={dailyData} />
         </div>
      </div>
   )
}
