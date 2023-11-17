// 'use client'
import GridComponent from './_Components/Grid'
import Chart from './_Components/Chart'
import getDailyData, { getHourlyData } from './_Components/API'

export default async function App() {
   const dailyData = await getDailyData()
   const hourlyData = await getHourlyData()

   return (
      <div className="bg-white h-full rounded-l-md my-4 ml-4 py-4 pl-5 pr-px">
         {/* <Chart props={data} /> */}
         <GridComponent props={dailyData} />
      </div>
   )
}
