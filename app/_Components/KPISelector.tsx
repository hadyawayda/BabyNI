'use client'

import { Dialog, Transition } from '@headlessui/react'
import { Fragment, useState } from 'react'
import { ReactChange, ReactEvent } from '../Interfaces/Interfaces'

type KPIprops = {
   onKPISelect: (KPIs: ReactEvent) => void
   selectedKPIs: object
}

const KPISelector = ({ onKPISelect, selectedKPIs }: KPIprops) => {
   let [isOpen, setIsOpen] = useState(false)

   function handleKPISelect(e: ReactChange) {
      onKPISelect(e.target)
   }

   return (
      <>
         <button
            className="whitespace-nowrap underline"
            onClick={() => setIsOpen(true)}
         >
            View Selected KPIs
         </button>
         <Transition
            appear
            show={isOpen}
            as={Fragment}
         >
            <Dialog
               as="div"
               className="relative z-10"
               onClose={() => setIsOpen(false)}
            >
               <Transition.Child
                  as={Fragment}
                  enter="ease-out duration-300"
                  enterFrom="opacity-0"
                  enterTo="opacity-100"
                  leave="ease-in duration-200"
                  leaveFrom="opacity-100"
                  leaveTo="opacity-0"
               >
                  <div
                     className="fixed inset-0 bg-slate-950/60"
                     aria-hidden="true"
                  />
               </Transition.Child>
               <div className="fixed inset-0 overflow-y-auto">
                  <div className="flex min-h-full items-center justify-center p-4 text-center">
                     <Transition.Child
                        as={Fragment}
                        enter="ease-out duration-300"
                        enterFrom="opacity-0 scale-95"
                        enterTo="opacity-100 scale-100"
                        leave="ease-in duration-200"
                        leaveFrom="opacity-100 scale-100"
                        leaveTo="opacity-0 scale-95"
                     >
                        <Dialog.Panel className="relative mx-auto p-9 rounded-2xl flex flex-col justify-start items-center bg-slate-100 dialog-panel">
                           <div className="flex flex-col justify-start items-center">
                              <Dialog.Title className="text-black">
                                 Select KPIs:
                              </Dialog.Title>
                           </div>
                           <div className="flex flex-col justify-between items-start w-1/2 my-10">
                              {Object.entries(selectedKPIs).map(
                                 ([key, value]) => (
                                    <div className="flex ml-4">
                                       <input
                                          type="checkbox"
                                          name={key}
                                          checked={value}
                                          onChange={handleKPISelect}
                                       />
                                       <label className="flex ml-8 text-black m-4">
                                          {key}
                                       </label>
                                    </div>
                                 )
                              )}
                           </div>
                           <button
                              onClick={() => setIsOpen(false)}
                              className="flex w-1/2 items-center h-12 justify-center rounded-3xl text-sm tracking-widest border bg-orange-600 hover:bg-white hover:text-orange-600 hover:border-orange-600 transition-colors duration-500"
                           >
                              SUBMIT
                           </button>
                        </Dialog.Panel>
                     </Transition.Child>
                  </div>
               </div>
            </Dialog>
         </Transition>
      </>
   )
}

export default KPISelector
