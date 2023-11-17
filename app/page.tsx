// 'use client'
import GridComponent from './_Components/Grid'
import Chart from './_Components/Chart'
import getDailyData, { getHourlyData } from './_Components/API'

export default async function App() {
   const dailyData = await getDailyData()
   const hourlyData = await getHourlyData()

   return (
      <div className="bg-white w-full h-full m-4 rounded-md">
         {/* <Chart props={data} /> */}
         <GridComponent props={dailyData} />
      </div>
   )
}
