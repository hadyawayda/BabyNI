import type { Metadata } from 'next'
import './globals.css'
import Navbar from './_Components/Navbar'
import Sidebar from './_Components/Sidebar'

export const metadata: Metadata = {
   title: 'Network Insight',
   description: 'Generated by create next app',
   authors: [{ name: 'Hady Awayda', url: 'https://hadyawayda.com' }],
}

export default function RootLayout({
   children,
}: {
   children: React.ReactNode
}) {
   return (
      <html lang="en">
         <body className="absolute w-full min-h-screen flex justify-between">
            <Sidebar />
            <main className="flex flex-col justify-between min-h-screen w-full items-center">
               <Navbar />
               {children}
            </main>
         </body>
      </html>
   )
}
